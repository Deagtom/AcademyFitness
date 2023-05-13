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
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для RegistrationCourseWindow.xaml
    /// </summary>
    public partial class RegistrationCourseWindow : Window
    {
        AcademyFitnessRamazanovEntities context;

        public RegistrationCourseWindow(AcademyFitnessRamazanovEntities context, CourseRegistration currentRegistration)
        {
            InitializeComponent();
            this.context = context;
            ComboBoxCourse.ItemsSource = context.Courses.ToList();
            ComboBoxTrainer.ItemsSource = context.Trainers.ToList();

            this.DataContext = currentRegistration;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckRegistration())
            {
                context.SaveChanges();
                this.Close();
            }
        }

        private bool CheckRegistration()
        {
            var registration = this.DataContext as CourseRegistration;
            if (registration.Trainer == null)
            {
                MessageBox.Show("Тренер не выбран");
                return false;
            }
            if (registration.Course == null)
            {
                MessageBox.Show("Курсы не выбраны");
                return false;
            }
            if (registration.CreatedDate == null)
            {
                MessageBox.Show("Дата не выбрана");
                return false;
            }
            if (!int.TryParse(TextTotalPoint.Text, out int result))
            {
                MessageBox.Show("Неверные данные");
                return false;
            }
            return true;
        }
    }
}
