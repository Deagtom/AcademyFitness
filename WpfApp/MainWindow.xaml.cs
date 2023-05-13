using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        AcademyFitnessRamazanovEntities context;

        public MainWindow()
        {
            InitializeComponent();
            context = new AcademyFitnessRamazanovEntities();
            RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();
            ComboBoxSelectTrainer.ItemsSource = context.Trainers.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newRegistration = new CourseRegistration();
            context.CourseRegistrations.Add(newRegistration);
            var window = new RegistrationCourseWindow(context, newRegistration);
            window.ShowDialog();
            RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var row = RegistrationGrid.SelectedItem as CourseRegistration;
            if (row == null)
            {
                MessageBox.Show("Строка не выбрана");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Вопрос", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                context.CourseRegistrations.Remove(row);
                context.SaveChanges();
                RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button EditButton = sender as Button;
            var currentRegistration = EditButton.DataContext as CourseRegistration;
            var window = new RegistrationCourseWindow(context, currentRegistration);
            window.ShowDialog();
        }

        private void FiltrButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationGrid.ItemsSource = context.CourseRegistrations.Where(x => x.IsDone == true).ToList();
            MessageBoxResult result = MessageBox.Show("Вернуть все записи?", "Вопрос", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();
            }
        }

        private void ComboBoxSelectTrainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxSelectTrainer.SelectedItem == null)
            {
                return;
            }
            var currentTrainer = (Trainer)ComboBoxSelectTrainer.SelectedItem;
            List<CourseRegistration> listTrainer = context.CourseRegistrations.ToList();
            RegistrationGrid.ItemsSource = listTrainer.Where(x => x.Trainer == currentTrainer).ToList();
        }

        private void CanselButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();
        }
    }
}