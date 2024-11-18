using AutoCompleteTextBoxDemo.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoCompleteTextBoxDemo.ViewModels
{
    /// <summary>
    /// Interaction logic for AutoCompleteTextBoxWindow.xaml
    /// </summary>
    public partial class AutoCompleteTextBoxWindow : Window, INotifyPropertyChanged
    {
        // Приватные поля для хранения данных
        private ObservableCollection<Product> _products;
        private ObservableCollection<Product> _filteredProducts;
        private string _filterText;

        // Свойство для доступа к коллекции исходных элементов
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(); // Оповещаем участников об изменении
            }
        }

        // Свойство для доступа к отфильтрованным элементам
        public ObservableCollection<Product> FilteredItems
        {
            get { return _filteredProducts; }
            set
            {
                _filteredProducts = value;
                OnPropertyChanged(); // Оповещаем участников об изменении
            }
        }

        // Свойство для хранения текста фильтра
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged(); // Оповещаем участников об изменении
                FilterItems(); // Вызываем метод фильтрации при изменении текста
            }
        }

        public AutoCompleteTextBoxWindow()
        {
            // Инициализация коллекции исходных элементов
            Products = new ObservableCollection<Product>
            {
                new Product(1, "Яблоко", 0.99m),
                new Product(2, "Бананы", 0.59m),
                new Product(3, "Вишня", 2.99m),
                new Product(4, "Груша", 1.49m),
                new Product(5, "Апельсин", 1.29m),
                new Product(6, "Мандарин", 1.19m),
                new Product(7, "Киви", 2.49m),
                new Product(8, "Ананас", 3.99m),
                new Product(9, "Дыня", 4.99m),
                new Product(10, "Арбуз", 5.99m)
            };

            // Инициализация отфильтрованных элементов как копии всех исходных элементов
            FilteredItems = new ObservableCollection<Product>(Products);
            DataContext = this; // Установка контекста данных для привязок в XAML
        }

        /// <summary>
        /// Метод для фильтрации элементов на основе текста фильтра.
        /// </summary>
        private void FilterItems()
        {
            FilteredItems.Clear(); // Очистка коллекции отфильтрованных элементов

            if (string.IsNullOrEmpty(FilterText)) // Если текст фильтра пуст или null
            {
                foreach (var item in Products) // Добавляем все элементы из исходной коллекции
                {
                    FilteredItems.Add(item);
                }
            }
            else // Если текст фильтра не пустой
            {
                foreach (var item in Products)
                {
                    // Проверяем, содержится ли текст фильтра в элементе (без учета регистра)
                    if (item.Name.ToLower().Contains(FilterText.ToLower()))
                    {
                        FilteredItems.Add(item); // Добавляем совпадающий элемент в отфильтрованную коллекцию
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик события MouseDown для окна.
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Проверяем, был ли клик вне StackPanel (stkpnl)
            if (e.OriginalSource is not DependencyObject dependencyObject || !grid.IsAncestorOf(dependencyObject))
            {
                scrlVis.IsChecked = false; // Убираем отметку с ToggleButton
                tb.Text = string.Empty; // Очищаем текст в TextBox
            }
        }

        /// <summary>
        /// Обработчик события TextChanged для TextBox.
        /// </summary>
        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterText = tb.Text; // Обновляем текст фильтра

            // Если текст не пустой, показываем ScrollViewer, иначе скрываем его при условии, что ToggleButton не отмечен
            if (FilterText != string.Empty)
                listbox.Visibility = Visibility.Visible;
            else if (scrlVis.IsChecked == false)
                listbox.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Обработчик события Checked для ToggleButton.
        /// </summary>
        private void scrlVis_Checked(object sender, RoutedEventArgs e)
        {
            listbox.Visibility = Visibility.Visible; // Показываем ScrollViewer при отметке ToggleButton
        }

        private void scrlVis_Unchecked(object sender, RoutedEventArgs e)
        {
            listbox.Visibility = Visibility.Hidden; // Скрываем ScrollViewer при снятии отметки с ToggleButton
        }

        // Метод для оповещения участников об изменении
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Событие, которое необходимо объявить при 
        // наследовании интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
