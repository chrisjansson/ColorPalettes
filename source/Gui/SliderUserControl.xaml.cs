using System.Windows;

namespace Gui
{
    public partial class SliderUserControl
    {
        public SliderUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof (double), typeof (SliderUserControl), new PropertyMetadata(default(double)));

        public double Minimum
        {
            get { return (double) GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof (double), typeof (SliderUserControl), new PropertyMetadata(default(double)));

        public double Maximum
        {
            get { return (double) GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (double), typeof (SliderUserControl), new PropertyMetadata(default(double)));

        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (SliderUserControl), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof (double), typeof (SliderUserControl), new PropertyMetadata(default(double)));

        public double Interval
        {
            get { return (double) GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }
    }
}
