using System;
using System.Linq;
using Automatonymous;
using Automatonymous.Binders;
using Geolocation.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Geolocation.Infrastructure.Saga
{
    public class AddressStateMachine : MassTransitStateMachine<AddressSaga>
    {
        public AddressStateMachine(ILogger<AddressStateMachine> logger)
        {
            Event(() => AddressNew, x => x.CorrelateById(x => x.Message.CorrelationId));

            Event(() => FaultAddressNew, x => x.CorrelateById(x => x.Message.Message.CorrelationId));

            Event(() => RemoveSaga, x => x.CorrelateById(x => x.Message.Message.CorrelationId));

            InstanceState(instance => instance.CurrentState);

            Initially(
                HandlerEvent(AddressNew)
                    .TransitionTo(Sending));

            DuringAny(
                HandlerError(FaultAddressNew)
                    .TransitionTo(Fault));

            DuringAny(
                When(RemoveSaga)
                    .Finalize());

            SetCompletedWhenFinalized();

            WhenEnterAny(state => state
                .Then(context =>
                {
                    context.Instance.DateUpdate = DateTime.Now;
                    logger.LogCritical("{Event} {@Saga}", state.Event, context.Instance);
                }));
        }

        /// <summary>
        /// Отправка в очередь
        /// </summary>
        private EventActivityBinder<AddressSaga, AddressNewEvent>
            HandlerEvent(Event<AddressNewEvent> @event) =>
            When(@event)
                .Then(ctx =>
                {
                    ctx.Instance.DateCreate = DateTime.Now;
                });

        /// <summary>
        /// Произошла ошибка
        /// </summary>
        private EventActivityBinder<AddressSaga, Fault<T>> HandlerError<T>(Event<Fault<T>> @event) where T : class =>
            When(@event: @event)
                .Then(ctx =>
                {
                    ctx.Instance.Description = ctx.Data.Exceptions.First().Message;
                });

        /// <summary>
        /// Отправка
        /// </summary>
        public State Sending { get; private set; }

        /// <summary>
        /// В ошибке
        /// </summary>
        public State Fault { get; private set; }

        
        /// <summary>
        /// Произошла ошибка 
        /// </summary>
        public Event<Fault<AddressNewEvent>> FaultAddressNew { get; private set; }

        /// <summary>
        /// Удаление 
        /// </summary>
        public Event<Fault<RemoveSagaEvent>> RemoveSaga { get; private set; }


        /// <summary>
        /// Новый Push 
        /// </summary>
        public Event<AddressNewEvent> AddressNew { get; private set; }

    }
}
