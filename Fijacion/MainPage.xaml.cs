using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Fijacion
{
    public partial class MainPage : ContentPage
    {
        double sacos, peso, tara, tarakg, taraqqs, Humedad;
        string productoSeleccionado;
        public MainPage()
        {
            InitializeComponent();
            crearlista();
        }
        private void crearlista()
        {
            // Crear la lista de elementos
            List<string> tiposCafe = new List<string>()
            {
                "POA 1RA",
                "PHA 1RA",
                "PMA 1RA",
                "BROZA OREADA",
                "BROZA HUMEDA",
                "BROZA MOJADA",
                "UVA MADURA ROBUSTA"
            };

            // Asignar la lista al Picker
            PProducto.ItemsSource = tiposCafe;
        }
        private void calcularPrecio()
        {
            if (double.TryParse(TxtSacos.Text, out sacos) && !string.IsNullOrEmpty(TxtPesoBruto.Text) && peso != 0)
            {
                //Calcular Tara
                tarakg = sacos * tara;
                taraqqs = tarakg / 46;
                LblTaraKG.Text = tarakg.ToString("N2");
                LblTaraQQs.Text = taraqqs.ToString("N2");
                //Calcular Peso Neto
                double kgnetos, qqsnetos;
                kgnetos = peso - tarakg;
                qqsnetos = kgnetos / 46;
                LblNetoKG.Text = kgnetos.ToString("N2");
                LblNetoQQs.Text = qqsnetos.ToString("N2");
                //Calcular Humedad
                double Humedadkg, Humedadqqs;
                Humedadkg = kgnetos * Humedad;
                Humedadqqs = Humedadkg / 46;
                LblHumedadKG.Text = Humedadkg.ToString("F2");
                LblHumedadQQs.Text = Humedadqqs.ToString("F2");
                //Calcular CONV POA
                double POAkg, POAqqs;
                POAkg = kgnetos - Humedadkg;
                POAqqs = POAkg / 46;
                LblConvPOAKG.Text = POAkg.ToString("N2");
                LblConvPOAQQs.Text = POAqqs.ToString("N2");
            }
            else
            {
                // Manejar el caso en que el precio o incentivo no sean números válidos
                DisplayAlert("Error", "Por favor ingrese valores numéricos válidos en Sacos y/o Peso Bruto.", "OK");
            }
        }

        private void Tara()
        {
            if (productoSeleccionado == "POA 1RA" ||
                productoSeleccionado == "PHA 1RA" ||
                productoSeleccionado == "BROZA OREADA" ||
                productoSeleccionado == "UVA MADURA ROBUSTA")
            {
                tara = 0.23;
                Humedad = 0;
            }
            else
            {
                tara = 0.38;
                Humedad = 0.14;
            }
        }
        private void Limpiar()
        {
            TxtPesoBruto.Text = null;
            TxtSacos.Text = "";
            LblQQs.Text = "--";
            LblTaraKG.Text = "--";
            LblTaraQQs.Text = "--";
            LblNetoKG.Text = "--";
            LblNetoQQs.Text = "--";
            LblHumedadKG.Text = "--";
            LblHumedadQQs.Text = "--";
            LblConvPOAKG.Text = "--";
            LblConvPOAQQs.Text = "--";
        }
        private void PProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del Picker
            productoSeleccionado = PProducto.SelectedItem?.ToString();
            Tara();
        }

        private void TxtPesoBruto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPesoBruto.Text != null)
            {
                peso = double.Parse(TxtPesoBruto.Text);
                double QQs = peso / 46;
                LblQQs.Text = QQs.ToString("N2");
            }
            else
            {
                peso = 0;
            }
        }

        private void BtnCalcular_Clicked(object sender, EventArgs e)
        {
            calcularPrecio();
        }

        private void BtnBorrar_Clicked(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
