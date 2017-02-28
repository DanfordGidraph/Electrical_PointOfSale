using System.Windows.Controls;
using System.Windows.Media;
namespace Electricals_PointOfSale.Views
   
{
    public class ButtonsStyle
    {

        public void styleAddingButtonsButton(Button[] btns, Button btn)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                if (btns[i].Content != btn.Content)
                {
                    ColorConverter converter = new ColorConverter();

                    RadialGradientBrush defBrush = new RadialGradientBrush();
                    GradientStop gstop1 = new GradientStop();
                    GradientStop gstop2 = new GradientStop();


                    gstop1.Color = Colors.Black;
                    gstop1.Offset = 0.992;
                    defBrush.GradientStops.Add(gstop1);

                    Color newColor = (Color)ColorConverter.ConvertFromString("#FF816504");
                    gstop2.Color = newColor;
                    gstop2.Offset = 0.238;
                    defBrush.GradientStops.Add(gstop2);

                    btns[i].Background = defBrush;
                    btns[i].Foreground = Brushes.Wheat;
                    btns[i].FontSize = 15;

                }

            }
            btn.Background = Brushes.Wheat;
            btn.Foreground = Brushes.Black;
            btn.FontSize = 20;
        }

        public void styleEditorButtons(Button[] btns, Button btn )
        {
            for (int i = 0; i < btns.Length; i++)
            {
                if (btns[i].Content != btn.Content)
                {
                    ColorConverter converter = new ColorConverter();

                    RadialGradientBrush defBrush = new RadialGradientBrush();
                    GradientStop gstop1 = new GradientStop();
                    GradientStop gstop2 = new GradientStop();


                    gstop1.Color = Colors.Black;
                    gstop1.Offset = 0.992;
                    defBrush.GradientStops.Add(gstop1);

                    Color newColor = (Color)ColorConverter.ConvertFromString("#FF816504");
                    gstop2.Color = newColor;
                    gstop2.Offset = 0.238;
                    defBrush.GradientStops.Add(gstop2);

                    btns[i].Background = defBrush;
                    btns[i].Foreground = Brushes.Wheat;
                    btns[i].FontSize = 15;

                }

            }
            btn.Background = Brushes.Wheat;
            btn.Foreground = Brushes.Black;
            btn.FontSize = 20;
        }

        public void styleMainWindowButtons(Button[] btns,Button btn)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                if (btns[i].Content != btn.Content)
                {
                    ColorConverter converter = new ColorConverter();

                    RadialGradientBrush defBrush = new RadialGradientBrush();
                    GradientStop gstop1 = new GradientStop();
                    GradientStop gstop2 = new GradientStop();


                    gstop1.Color = Colors.Black;
                    gstop1.Offset = 0.992;
                    defBrush.GradientStops.Add(gstop1);

                    Color newColor = (Color)ColorConverter.ConvertFromString("#FF816504");
                    gstop2.Color = newColor;
                    gstop2.Offset = 0.238;
                    defBrush.GradientStops.Add(gstop2);

                    btns[i].Background = defBrush;
                    btns[i].Foreground = Brushes.Wheat;
                    btns[i].FontSize = 15;

                }

            }
            btn.Background = Brushes.Yellow;
            btn.Foreground = Brushes.Black;
        }

    }
}
