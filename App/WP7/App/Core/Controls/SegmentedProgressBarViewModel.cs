using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TexasHoldemCalculator.Core.Controls
{
    /// <summary>
    /// An attached view model that adapts a ProgressBar control to provide properties
    /// that assist in the creation of a segmented circular template
    /// </summary>
    public class SegmentedProgressBarViewModel : CircularProgressBarViewModel
    {
        private int _segmentCount = 8;

        private List<SegmentData> _segments;

        public int SegmentCount
        {
            get { return this._segmentCount; }
            set
            {
                this._segmentCount = value;
                this.BuildSegments();
                this.ComputeViewModelProperties();
            }
        }

        public List<SegmentData> Segments
        {
            get { return this._segments; }
            set { this._segments = value; OnPropertyChanged("Segments"); }
        }

        public SegmentedProgressBarViewModel()
        {
            this.BuildSegments();
        }

        private void BuildSegments()
        {
            var segments = new List<SegmentData>();
            double endAngle = 360.0 / (double)this.SegmentCount;
            for (int i = 0; i < this.SegmentCount; i++)
            {
                double startAngle = (double)i * 360 / (double)this.SegmentCount;
                segments.Add(new SegmentData(startAngle, endAngle, this));
            }

            this.Segments = segments;
        }

        protected override void ComputeViewModelProperties()
        {
            if (_progressBar == null)
                return;

            double normalValue = _progressBar.Value / (_progressBar.Maximum - _progressBar.Minimum);

            for (int i = 0; i < this.SegmentCount; i++)
            {
                double startValue = (double)i / (double)this.SegmentCount;
                double endValue = (double)(i + 1) / (double)this.SegmentCount;
                double opacity = (normalValue - startValue) / (endValue - startValue); ;
                opacity = Math.Min(1, Math.Max(0, opacity));
                this._segments[i].Opacity = opacity;
            }
            base.ComputeViewModelProperties();
        }


        /// <summary>
        /// The data for an individual segment.
        /// </summary>
        public class SegmentData : INotifyPropertyChanged
        {
            public double StartAngle { get; private set; }

            public double WedgeAngle { get; private set; }

            private double _opacity;

            public double Opacity
            {
                get { return this._opacity; }
                set { this._opacity = value; this.OnPropertyChanged("Opacity"); }
            }

            public SegmentedProgressBarViewModel Parent { get; private set; }

            public SegmentData(double start, double wedge,
              SegmentedProgressBarViewModel parent)
            {
                this.StartAngle = start;
                this.WedgeAngle = wedge;
                this.Parent = parent;
                this.Opacity = 0;
            }

            #region INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string property)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }

            #endregion
        }
    }
}