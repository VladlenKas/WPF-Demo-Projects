using AutoCompleteTextBoxDemo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace AutoCompleteTextBoxDemo.ViewModels
{
    /// <summary>
    /// Логика взаимодействия для AutoCompleteComboBoxWindow.xaml
    /// </summary>
    public partial class AutoCompleteComboBoxWindow : Window, INotifyPropertyChanged
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
        public ObservableCollection<Product> FilteredProducts
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

        public AutoCompleteComboBoxWindow()
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
            FilteredProducts = new ObservableCollection<Product>(Products);
            DataContext = this; // Установка контекста данных для привязок в XAML
        }

        // Метод для фильтрации элементов на основе текста фильтра
        private void FilterItems()
        {
            FilteredProducts.Clear(); // Очистка коллекции отфильтрованных элементов

            if (string.IsNullOrEmpty(FilterText)) // Если текст фильтра пуст или null
            {
                foreach (var product in Products) // Добавляем все элементы из исходной коллекции
                {
                    FilteredProducts.Add(product);
                }
            }
            else // Если текст фильтра не пустой
            {
                foreach (var product in Products)
                {
                    // Проверяем, содержится ли текст фильтра в элементе (без учета регистра)
                    if (product.Name.ToLower().Contains(FilterText.ToLower()))
                    {
                        FilteredProducts.Add(product); // Добавляем совпадающий элемент в отфильтрованную коллекцию
                    }
                }
            }
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
