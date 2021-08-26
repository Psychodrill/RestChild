using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace RestChild.MPGUIntegration
{
    [GeneratedCode("System.Xml", "4.7.3190.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    public sealed class ServiceProperties : object, INotifyPropertyChanged
    {
        private long purposeCode;

        private string purposeValue;

        private long kidsCountCode;

        private string kidsCountValue;

        private long bookingId;

        private DateTime slot;

        [XmlElement(Order = 1)]
        public long PurposeCode
        {
            get => this.purposeCode;
            set
            {
                this.purposeCode = value;
                this.RaisePropertyChanged(nameof(PurposeCode));
            }
        }

        [XmlElement(Order = 2)]
        public string PurposeValue
        {
            get => this.purposeValue;
            set
            {
                this.purposeValue = value;
                this.RaisePropertyChanged(nameof(PurposeValue));
            }
        }

        [XmlElement(Order = 3)]
        public long KidsCountCode
        {
            get => this.kidsCountCode;
            set
            {
                this.kidsCountCode = value;
                this.RaisePropertyChanged(nameof(KidsCountCode));
            }
        }

        [XmlElement(Order = 4)]
        public string KidsCountValue
        {
            get => this.kidsCountValue;
            set
            {
                this.kidsCountValue = value;
                this.RaisePropertyChanged(nameof(KidsCountValue));
            }
        }

        [XmlElement(Order = 5)]
        public long BookingId
        {
            get => this.bookingId;
            set
            {
                this.bookingId = value;
                this.RaisePropertyChanged(nameof(BookingId));
            }
        }

        [XmlElement(Order = 6, ElementName = "slot")]
        public DateTime Slot
        {
            get => this.slot;
            set
            {
                this.slot = value;
                this.RaisePropertyChanged(nameof(Slot));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
