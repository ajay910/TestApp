namespace Aglive.Business.Infrastructure.Utilities
{
    using GalaSoft.MvvmLight.Views;

    public interface IAgliveDialogService : IDialogService
    {
        void ToastMessage(string message);
    }
}