# GlobalEarthquakeMonitor

## Introduction
The global earthquake monitor is a desktop application that displays information regarding worldwide earthquake activity. When first launched, the application displays an user interface that contains a list of all earthquakes that have occurred in the last one hour. 

This information includes the occurrence time, magnitude, coordinates and closest cities. Subsequently, the application periodically (default time period is 20 seconds) requests and displays the latest information regarding earthquake activity. 

The application also has a configuration file that can be used to change some application settings, for example: the frequency at which the real time earthquake data should be updated.

## Deployment
Pre-requisites: The application is built using the .NET version 4.5 as the platform. Therefore, this needs to be installed prior to running the application. The other dependent files are automatically installed and used locally by the application.

### Installer: 
The Windows installer (MSI) for the application can be downloaded from the project repository's setup folder:
https://github.com/venkatt76/GlobalEarthquakeMonitor/tree/master/GlobalEarthquakeMonitor/Setup/bin/x64/Release

The installer has been provided for automated deployment of the application. When launched, the installer displays a user interface that can be used to specify the directory for the installation, the default installation folder is:
**%ProgramFiles%\Plethora\GlobalEarthquakeMonitor 1.0**

The file **Plethora.GlobalEarthquakeMonitor.exe** is the application executable which can be launched from its install location.

### XCopy Deployment:
Since the application is a standalone executable, the executable and dependent files can simply be copied to a local folder and then launched from that location, if desired. There are no other actions performed by the installer in addition to the application files being copied to the target folder (i.e. no custom actions, configuration etc.).

The application and dependent files can be dowloaded from the project repository's application folder:
https://github.com/venkatt76/GlobalEarthquakeMonitor/tree/master/GlobalEarthquakeMonitor/Application/bin/Release

This folder (all files and subfolders) can be copied to a local folder to deploy the application locally. The application executable - Plethora.GlobalEarthquakeMonitor.exe can then be launched from this folder. 

## User Interface:
The application displays a simple user interface that displays a list of the worldwide earthquakes in the last hour upon initial launch. The time at which this information was obtained is also displayed. The information about earthquakes consists of:
  - Occurrence time (UTC)
  - Magnitude
  - Coordinates (latitude, longitude)
  - Closest cities (three cities located geographically closest to the earthquake)

The application uses the real-time feeds made available by USGS. The feeds are accessible programmatically using web service requests and return the data in [GeoJSON] (http://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php) format. The application parses the GeoJSON data to retrieve and display the earthquake data. The application also periodically requests for the latest information regarding earthquake activity and updates the list accordingly. The synchronization time is also updated to indicate when the latest information was obtained.

## Configuration:
The configuration file for the application - Plethora.GlobalEarthquakeMonitor.exe.config is installed/copied to the same location as the application file. This file can be edited using a text editor to view the application settings. The settings include the earthquake data feed URI, time interval in between updates (in seconds).

## Build Instructions:
The application source code consists of a solution that can be opened using Microsoft Visual Studio 2015. The solution contains three projects namely:
  - Plethora.GlobalEarthquakeMonitor - Application project
  - Plethora.GlobalEarthquakeMonitor.Setup - Setup (Installation) project
  - Plethora.GlobalEarthquakeMonitor.UnitTests - UnitTests project

### Pre-requisites:
The application is built using the .NET version 4.5 as the platform. Therefore, this needs to be installed in order to successfully build the application. The setup project is built using the WIX platform, there this needs to be installed when building the Release configuration of the solution (the Debug configuration, by default, does not compile the setup project). The latest release of WIX can be installed from [WIX Toolset] (http://wixtoolset.org/releases/).

### Application details:
  - .NET framework 4.5 (platform), C# (programming language), WPF (user interface), MVVM (UI pattern), WIX (installer).
  - Third-party libraries (NuGet packages): Newtonsoft.dll (JSON parser) - already present in packages subfolder.
  - Data files: world_cities.csv (world cities names and coordinates file) - already present in Data subfolder.
