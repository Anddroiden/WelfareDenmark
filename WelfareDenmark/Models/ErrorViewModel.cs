using System.ComponentModel;

namespace WelfareDenmark.Models {
    public class ErrorViewModel {

        [DisplayName("Request Error: Please try again.")]
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}