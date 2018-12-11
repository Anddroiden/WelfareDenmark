using System.ComponentModel;

namespace WelfareDenmark.Models {
    public class ErrorViewModel {

        [DisplayName("Request fejl: venligst prøv igen.")]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}