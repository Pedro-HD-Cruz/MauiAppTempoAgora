using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    try
                    {
                        Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                        if (t != null)
                        {
                            string dados_previsao = "";

                            dados_previsao = $"Latitude: {t.lat} \n" +
                                             $"Longitude: {t.lon} \n" +
                                             $"Nascer do Sol: {t.sunrise} \n" +
                                             $"Por do Sol: {t.sunset} \n" +
                                             $"Temp Máx: {t.temp_max} \n" +
                                             $"Temp Min: {t.temp_min} \n" +
                                             $"Descrição: {t.description} \n" +
                                             $"Velocidade do Vento: {t.speed} \n" +
                                             $"Visibilidade: {t.visibility} metros";

                            lbl_resultado.Text = dados_previsao;

                        }
                        else
                        {
                            lbl_resultado.Text = "Sem dados de Previsão";
                        }

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cidade não encontrada"))
                        {
                            await DisplayAlert("Erro", "Cidade não encontrada. Por favor, verifique o nome digitado.", "OK");
                        }
                        else if (ex.Message.Contains("Erro de conexão com a internet"))
                        {
                            await DisplayAlert("Erro de Conexão", "Verifique sua conexão com a internet e tente novamente.", "OK");
                        }
                        else
                        {
                            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                        }
                        lbl_resultado.Text = "";
                    }
                }
                else
                {
                    lbl_resultado.Text = "Preencha a cidade.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }

}
