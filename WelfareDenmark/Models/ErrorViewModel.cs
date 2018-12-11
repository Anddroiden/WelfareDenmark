using System.ComponentModel;

namespace WelfareDenmark.Models {
    public class ErrorViewModel {

        [DisplayName("Request fejl: venligst pr�v igen.")]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}