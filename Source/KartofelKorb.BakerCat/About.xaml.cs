using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace KartofelKorb.BakerCat
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        private void OnLoveClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private void OnHateClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "Baker Cat! Feedback";
            emailComposeTask.Body = "Baker Cat! is great, but ";
            emailComposeTask.To = "kartofelkorb@hotmail.com";

            emailComposeTask.Show();
        }
    }
}