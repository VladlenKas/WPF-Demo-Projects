using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace AutoCompleteTextBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Приватные поля для хранения данных
        private ObservableCollection<string> _items;
        private string _filterText;
        private ObservableCollection<string> _filteredItems;

        // Свойство для доступа к коллекции исходных элементов
        public ObservableCollection<string> Items
        {
            get => _items;
            set => _items = value;
        }

        // Свойство для хранения текста фильтра
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                FilterItems(); // Вызываем метод фильтрации при изменении текста
            }
        }

        // Свойство для доступа к отфильтрованным элементам
        public ObservableCollection<string> FilteredItems
        {
            get => _filteredItems;
            set => _filteredItems = value;
        }

        public MainWindow()
        {
            Items = new ObservableCollection<string>
            {
                "Авокадо",
                "Мандарин",
                "Киви",
                "Банан",
                "Яблоко",
                "Груша",
                "Гагасик"
            };

            // Инициализация отфильтрованных элементов как копии всех исходных элементов
            FilteredItems = new ObservableCollection<string>(Items);
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
                foreach (var item in Items) // Добавляем все элементы из исходной коллекции
                {
                    FilteredItems.Add(item);
                }
            }
            else // Если текст фильтра не пустой
            {
                foreach (var item in Items)
                {
                    // Проверяем, содержится ли текст фильтра в элементе (без учета регистра)
                    if (item.ToLower().Contains(FilterText.ToLower()))
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
            if (e.OriginalSource is not DependencyObject dependencyObject || !stkpnl.IsAncestorOf(dependencyObject))
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
                scrl.Visibility = Visibility.Visible;
            else if (scrlVis.IsChecked == false)
                scrl.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Обработчик события Checked для ToggleButton.
        /// </summary>
        private void scrlVis_Checked(object sender, RoutedEventArgs e)
        {
            scrl.Visibility = Visibility.Visible; // Показываем ScrollViewer при отметке ToggleButton
        }

        private void scrlVis_Unchecked(object sender, RoutedEventArgs e)
        {
            scrl.Visibility = Visibility.Hidden; // Скрываем ScrollViewer при снятии отметки с ToggleButton
        }
    }
}