using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
namespace Photo_Gallery_BPA_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int currentPhotoNumber;
        public string[] galleryBox;
        public MainWindow()
        {
            InitializeComponent();
            SetUp();
        }
        // We'll configure and set up the program for start up here.
        public void SetUp()
        {
            this.Title = "Photo Gallery";
            leftButton.IsEnabled = false;
            rightButton.IsEnabled = false;
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            YesOrNoExit();
        }

       // We will use this code for confirmation to make sure the user wants to exit the program.
        public void YesOrNoExit()
        {
            var dlgResult = System.Windows.MessageBox.Show("Are you sure you wish to exit the photo gallery?", "Confirm", MessageBoxButton.YesNo);
            if (dlgResult.ToString() == "Yes")
            {
                Environment.Exit(0);
            }

        }

        private void loadFolder_Click(object sender, RoutedEventArgs e)
        {
            string SelectedFolder;
            // This menu will launch the menu to choose folders. Here, the users get to pick which gallery folder to choose.
            var dialog = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            // this if statement is here to make sure the user ACTUALLY picked a folder and not just exited out.
            if (result.ToString() == "OK")
            {
                SelectedFolder = dialog.SelectedPath.ToString();
            }
            else
            {
                return;
            }

           GrabPictures(SelectedFolder);
        }
        public void GrabPictures(string folder)
        {
            // We have to enable the buttons when the user selectes a photo, since they're disabled at start-up.
            if (leftButton.IsEnabled == false || rightButton.IsEnabled == false)
            {
                leftButton.IsEnabled = true;
                rightButton.IsEnabled = true;
            }
            // This function will get the files in the given folder location.
            galleryBox = Directory.GetFiles(folder);
            this.Title = $"Photo Gallary - {folder}";
            // We will use this to keep count where the users currently is/viewing.
            currentPhotoNumber = 0;
            // This function will show the first picture.
            mainPicture.Source = new BitmapImage(new Uri($@"{galleryBox[0]}"));
            // Show users where their current position is at and the amount of photos there are.
            amount.Text = $"{currentPhotoNumber+1}/{galleryBox.Length.ToString()}";

        }

        private void rightButton_Clicked(object sender, RoutedEventArgs e)
        {
            // We use this if statement to make sure when we go out of range, we go back to the start.
            if (currentPhotoNumber + 1 == galleryBox.Length)
            {
                currentPhotoNumber = -1;
            }
            currentPhotoNumber += 1;
            mainPicture.Source = new BitmapImage(new Uri($@"{galleryBox[currentPhotoNumber]}"));
            amount.Text = $"{currentPhotoNumber + 1}/{galleryBox.Length.ToString()}";
        }

        private void leftButton_Click(object sender, RoutedEventArgs e)
        {
            // We use this if statement here to make sure if we go back to far, we go to the end of the queue.
            if (currentPhotoNumber - 1 == -1)
            {
                currentPhotoNumber = galleryBox.Length;
            }
            currentPhotoNumber -= 1;
            mainPicture.Source = new BitmapImage(new Uri($@"{galleryBox[currentPhotoNumber]}"));
            amount.Text = $"{currentPhotoNumber + 1}/{galleryBox.Length.ToString()}";
        }
    }
}
