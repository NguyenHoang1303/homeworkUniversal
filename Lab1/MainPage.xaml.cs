using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private string checkedGender;
        private string checkedCalendarDatePicker;
        public int countError;

       

        public MainPage()
        {
            this.InitializeComponent();

        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(84|0[3|5|7|8|9])+([0-9]{8})$").Success;

        }

        public static bool IsEmail(string email)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            return Regex.IsMatch(email, pattern);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            checkedGender = rb.Content.ToString();
        }

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            CalendarDatePicker cdp = sender as CalendarDatePicker;
            checkedCalendarDatePicker = cdp.Date.Value.ToString("dd-MM-yyyy");
        }

        private void checkValidate(string FirstName, string LastName, string Avatar, string Password,
            string Address, string checkedGender, string checkedCalendarDatePicker, string Email)
        {
             countError = 0;

            if (String.IsNullOrEmpty(FirstName))
            {
                error_fname.Text = "First name is not empty";
                countError++;
            }
            else
            {
                error_fname.Text = "";
              
            }
            if (String.IsNullOrEmpty(LastName))
            {
                error_lname.Text = "Last name is not empty";
                countError++;
            }
            else
            {
                error_lname.Text = "";
            }
           
            if (String.IsNullOrEmpty(Avatar))
            {
                error_avatar.Text = "Avatar is not empty";
                countError++;
            }
            else
            {
                error_avatar.Text = "";
            }
            if (String.IsNullOrEmpty(Password))
            {
                error_password.Text = "Password is not empty";
                countError++;
            }
            else
            {
                error_password.Text = "";
            }
            if (String.IsNullOrEmpty(Address))
            {
                error_address.Text = "Email is not empty";
                countError++;
            }
            else
            {
                error_address.Text = "";
            }
            if (String.IsNullOrEmpty(Email))
            {
                error_email.Text = "Email is not empty";
                countError++;
            }
            else
            {
                if (!IsEmail(Email))
                {
                    error_email.Text = "Đây không phải là email";
                    countError++;
                }
                else
                {
                    error_email.Text = "";
                }
            }
            
            if (String.IsNullOrEmpty(checkedGender))
            {
                error_gender.Text = "Gender is not empty";
                countError++;
            }
            else
            {
                error_gender.Text = "";
            }
            if (String.IsNullOrEmpty(checkedCalendarDatePicker))
            {
                error_bday.Text = "Birthday is not empty";
                countError++;
            }
            else
            {
                error_bday.Text = "";

            }
        }
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            checkValidate(fname.Text, lname.Text, avatar.Text,
                password.Password.ToString(), address.Text, 
                checkedGender, checkedCalendarDatePicker, email.Text);

            if (countError > 0)
            {
                return;
            }
            {
                var register = new
                {
                    firstName = fname.Text,
                    lastName = lname.Text,
                    Password = password.Password.ToString(),
                    Address = address.Text,
                    Avatar = address.Text,
                    Gender = checkedGender,
                    Email = email.Text,
                    Birthday = checkedCalendarDatePicker,
                    Introduction = introduction.Text,
                };
                var jsonString = JsonConvert.SerializeObject(register);
                Debug.WriteLine(register);
                ContentDialog contentDialog = new ContentDialog();
                contentDialog.Title = "Thông tin đăng ký dạng Json";
                contentDialog.Content = jsonString;
                contentDialog.CloseButtonText = "OK";
                await contentDialog.ShowAsync();
                return;
            }
        }
    }
}
