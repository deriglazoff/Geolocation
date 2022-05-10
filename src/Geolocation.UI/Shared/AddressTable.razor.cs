using System.Text.RegularExpressions;
using Geolocation.UI.Data;
using Geolocation.UI.ModelsView;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Geolocation.UI.Shared
{
    public partial class AddressTable
    {
        [Inject] private ISnackbar Snackbar { get; set; }

        [Inject] AddressService ForecastService { get; set; }
        
        private List<AddressViewModel> Data { get; set; }

        private string searchString = "";

        private bool _loading;
        private bool FilterFunc(AddressViewModel element)
        {
            Regex regex = new(searchString, RegexOptions.IgnoreCase);
            if (regex.IsMatch(element.CorrelationId.ToString() ?? string.Empty))
                return true;
            if (regex.IsMatch(element.Value))
                return true;
            return false;
        }

        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            Data = await ForecastService.GetForecastAsync();
            await Refresh();
        }
        public Task Refresh()
        {
            _loading = false;
            return Task.CompletedTask;
        }
        private async Task EditClick(AddressViewModel context)
        {
        }

        private async Task RemoveClick(AddressViewModel context)
        {
            try
            {
                Data.Remove(context);
                Snackbar.Add($"Удалено {context.CorrelationId}", Severity.Success);

                await Refresh();
            }
            catch (Exception e)
            {
                Snackbar.Add($"Ошибка {e.Message}", Severity.Warning);
            }
        }
    }
}
