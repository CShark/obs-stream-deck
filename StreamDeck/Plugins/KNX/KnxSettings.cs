﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace StreamDeck.Plugins.KNX {
    /// <summary>
    /// Modes to connect to the KNX endpoint
    /// </summary>
    public enum KnxMode {
        Routing,
        Tunneling
    }

    /// <summary>
    /// Supported Datapoint Types for GroupAddress endpoints
    /// </summary>
    public enum KnxDatapointType {
        Dpt1, // Binary datapoint
        Dpt5, // Relative value between 0 and 255, mapped to group min and max
    }

    /// <summary>
    /// Definitions for a single group
    /// </summary>
    public class KnxGroup {
        /// <summary>
        /// The group address, either in three level or two level format
        /// </summary>
        public string GroupAddress { get; set; }

        /// <summary>
        /// A Human-Readable name for the group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the datapoint to send
        /// </summary>
        public KnxDatapointType Type { get; set; }
    }

    /// <summary>
    /// Global settings for the KNX connection
    /// </summary>
    public class KnxSettings : INotifyPropertyChanged {
        private KnxMode _mode = KnxMode.Routing;
        public string IP { get; set; }

        public int Port { get; set; } = 3671;

        /// <summary>
        /// Local address, used in tunnel-mode
        /// </summary>
        public string LocalIP { get; set; }

        /// <summary>
        /// Local port, used in tunnel-mode
        /// </summary>
        public int LocalPort { get; set; }

        public KnxMode Mode {
            get => _mode;
            set {
                if (value == _mode) return;
                _mode = value;
                OnPropertyChanged();
            }
        }

        public bool ThreeLevelGroupAdressing { get; set; } = true;

        /// <summary>
        /// A list of KNX-groups which are available to choose from
        /// </summary>
        public ObservableCollection<KnxGroup> Groups { get; set; } = new();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class KnxSlotGroup {
        public KnxGroup Group { get; set; }
        public byte[] OnEntry { get; set; } = new byte[0];
        public byte[] OnExit { get; set; } = new byte[0];
    }

    /// <summary>
    /// 
    /// </summary>
    public class KnxSlotSettings {
        public ObservableCollection<KnxSlotGroup> Groups { get; set; } = new();
    }
}