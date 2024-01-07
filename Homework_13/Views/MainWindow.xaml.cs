﻿//using Homework_13.Models.Client;
using Bank.Domain.Client;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Homework_13;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// метод фильтрации клиентов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ClientCollectionFilter(object sender, FilterEventArgs e)
    {
        if (!(e.Item is Client client)) return;
        if (client.Firstname is null || client.Lastname is null) return;

        var filter_text = ClientFilter.Text;
        if (filter_text.Length == 0) return;

        if (client.Firstname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
        if (client.Lastname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

        e.Accepted = false;
    }

    /// <summary>
    /// метод обновления списка на лету
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnClientFilterTextChanget(object sender, TextChangedEventArgs e)
    {
        var text_box = (TextBox)sender;
        var collection = (CollectionViewSource)text_box.FindResource("ClientCollection");
        collection.View.Refresh();
    }
}
