namespace BlazorAccountManagement.Services
{
    public class ModalService
    {
        public event Action<string> OnToggleSuccessModal;
        public event Action<string> OnToggleErrorModal;


        public void ToggleSuccessModal(string message = null)
        {
            OnToggleSuccessModal?.Invoke(message);
        }

        public void ToggleErrorModal(string message)
        {
            OnToggleErrorModal?.Invoke(message);
        }
    }
}
