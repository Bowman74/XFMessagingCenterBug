using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFMessagingCenterBug
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool exitLoop = false;

            try
            {
                Task.Run(() =>
                {
                    do
                    {
                        try
                        {
                            MessagingCenter.Subscribe<MainPage, string>(this, $"test{0}", (a, b) => { });
                            MessagingCenter.Unsubscribe<MainPage, string>(this, $"test{0}");
                        }
                        catch (Exception ex1)
                        {
                            Debug.Print($"Error Occurred: {ex1}");
                        }
                    } while (!exitLoop);
                });

                Task.Run(() =>
                {
                    do
                    {
                        try
                        {
                            MessagingCenter.Subscribe<MainPage, string>(this, $"test{0}", (a, b) => { });
                            MessagingCenter.Unsubscribe<MainPage, string>(this, $"test{0}");
                        }
                        catch (Exception ex2)
                        {
                            Debug.Print($"Error Occurred: {ex2}");
                        }

                    } while (!exitLoop);
                });
                
                await DisplayAlert("Done", $"Waiting for errors", "OK");
            }
            finally
            {
                exitLoop = true;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            bool exitLoop = false;
            var semaphore = new SemaphoreSlim(1, 1);

            try
            {
                Task.Run(() =>
                {
                    do
                    {
                        try
                        {
                            semaphore.Wait();

                            MessagingCenter.Subscribe<MainPage, string>(this, $"test{0}", (a, b) => { });
                            MessagingCenter.Unsubscribe<MainPage, string>(this, $"test{0}");
                        }
                        catch (Exception ex)
                        {
                            Debug.Print($"Error Occurred: {ex}");
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    } while (!exitLoop);
                });

                Task.Run(() =>
                {
                    do
                    {
                        try
                        {
                            semaphore.Wait();

                            MessagingCenter.Subscribe<MainPage, string>(this, $"test{0}", (a, b) => { });
                            MessagingCenter.Unsubscribe<MainPage, string>(this, $"test{0}");
                        }
                        catch (Exception ex)
                        {
                            Debug.Print($"Error Occurred: {ex}");
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    } while (!exitLoop);
                });

                await DisplayAlert("Done", $"Waiting for errors", "OK");
            }
            finally
            {
                exitLoop = true;
            }
        }
    }
}
