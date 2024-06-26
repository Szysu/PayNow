using System;
using Ab4d.SharpEngine.Assimp;
using Ab4d.SharpEngine.AvaloniaUI;
using Ab4d.SharpEngine.Cameras;
using Ab4d.SharpEngine.Common;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using PayNow.Infrastructure.Entities;
using PayNow.ViewModels;

namespace PayNow.Views;

public partial class ScootersListView : ReactiveUserControl<ScootersListViewModel>
{
    private readonly AssimpImporter _modelImporter = new();

    public ScootersListView()
    {
        InitializeComponent();
        SetupScene();
    }

    private void SetupScene()
    {
        SetupCamera();

        MainSceneView.BackgroundColor = new(255, 255, 255, 255);

        Unloaded += (_, _) => MainSceneView.Dispose();
    }

    private void SetupCamera()
    {
        MainSceneView.SceneView.Camera = new TargetPositionCamera
        {
            Heading = 50,
            Attitude = -30,
            Distance = 6000,
            ViewWidth = 100,
            TargetPosition = new(0, 0, 0),
            ShowCameraLight = ShowCameraLightType.Always
        };

        MainSceneView.SceneUpdating += (_, _) => { MainSceneView?.SceneView.Camera?.RotateCamera(0.5f, 0f); };

        _ = new PointerCameraController(MainSceneView)
        {
            ZoomMode = CameraZoomMode.CameraRotationCenterPosition,
            RotateAroundPointerPosition = true
        };
    }

    private void ChangeModel(Scooter scooter)
    {
        MainSceneView.Scene.RootNode.Clear();

        try
        {
            var scooterModel = _modelImporter.Import($"Assets/ScootersModels/{scooter.Manufacturer}/{scooter.Manufacturer}.fbx");

            if (scooterModel is null)
            {
                return;
            }

            MainSceneView.Scene.RootNode.Add(scooterModel);
        }
        catch (Exception)
        {
            // Ignored
        }
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ChangeModel((Scooter) e.AddedItems[0]!);
        ViewModel?.SaveChanges();
    }

    private void AddScooter(object? sender, RoutedEventArgs e)
    {
        var newScooter = new Scooter();
        ViewModel?.Scooters.Add(newScooter);
        Grid.SelectedItem = newScooter;
    }

    private void RemoveScooter(object? sender, RoutedEventArgs e)
    {
        ViewModel?.Scooters.Remove((Scooter) Grid.SelectedItem);
        ViewModel?.SaveChanges();
    }
}