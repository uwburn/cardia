[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NYEKDVZMWSLJG)

# Cardia
## Heart rate monitoring client for gamers

![Cardia banner](https://raw.githubusercontent.com/uwburn/cardia/master/Readme/cardia1small.png)

_Concept by Valentina Bordin, development by Michele Tibaldi._

Cardia is the first Heart Rate Monitor designed for gaming and broadcasting: it will display your current heart rate and generates a simulated ECG tracing, perfect for adding an original touch to gaming videos and livestreams.
But the main feature is the multiplayer support: if you and your friends have the right setup you can connect and share your vitals, allowing a co-op experience like never before.

_**NOTE:** This software and the simulated ECG are intended for entertainment purposes only. The ECG is a simulation based on the current heart rate and it has nothing to do with the real thing._

- - - -

### Supported devices
Cardia currently support the following devices:
* Zephyr HxM
* Contec CMS50
* Bluetooth Smart HRP compliant devices

- - - -

### Installation
Just download the zip from the releases pages, uncompress it and run Cardia.exe.
The Cardia main version supports only Windows 10 version 1709 onwards. Windows 8.1 should be also compatible, but i cannot test it out at the moment. Windows 10 versions before 1709 are known to have problem with Bluetooth Smart, Cardia will run, but communication with Bluetooth Smart devices will not.
The legacy version cuts out Bluetooth Smart Heart Rate Profile support, so Windows XP and later should be supported. At the moment it has been tested only on Windows 7, Windows 8, Windows 8.1 and Windows 10.

- - - -

### User guide

#### Main interface

![Main interface](https://raw.githubusercontent.com/uwburn/cardia/master/Readme/interface.png)

Let's take a closer look at the interface:
* Right click on the main window will give the option to shrink and unshrink it for better aesthetics. When the window is unshrinked, width can be expanded if you want a larger chart 
* The first option on the left offers alternative colors for the graphics.
* The second option changes the ECG displayed time from 1 second to 10.
* Volume bar will set the sound levels.
* The start button on the right will start the bluetooth data reading or the emulation. 

#### Device selection and configuration
This drop-down menu allows the user to select a proper device. Once the device is selected, it should be configured. 
When Zephyr HxM is selected, the configuration button will open a new window. The correct serial port for the device should be selected and after starting the application, the device status will display the relevant data in every packet received. The RR intervals are measured in milliseconds. Different devices have different configuration windows, but they look pretty similar.

![Zephyr device window](https://raw.githubusercontent.com/uwburn/cardia/master/Readme/zephyrdevice.png)

When the emulator is selected, the configuration button will open the emulator configuration windows. The user can select a range from a minimum to a maximum beats per minutes. The emulator will generate numbers comprised within given limits.

![Emulator device window](https://raw.githubusercontent.com/uwburn/cardia/master/Readme/emulatordevice.png)

#### Alarm
The alarm button will open a new window. Ticking the "enable alarm" box allows the user to set an alarm for both high heart rates or low heart rates. The default settings are 40 bpm and 180 bpm, but they can be changed by the user at any time. 
With the defuse alarm box checked, the alarm will repeat for a set number of seconds (10 seconds is the default setting)
If the alarm sound is enabled in the sound dropdown menu, an alarm sound will be played, otherwise only the "alarm" text will appear in the BPM box.

![Alarm window](https://raw.githubusercontent.com/uwburn/cardia/master/Readme/alarm.png)

#### Sound
This dropdown menu will allow the user to choose which sounds will be played by Cardia. The user can choose to play a sound for each heartbeat (play beat), or an alarm sound for high/low heart rate (play alarm), or both.

![Sound menu](https://raw.githubusercontent.com/uwburn/cardia/master/Readme/sound.png)

#### Log
If logging is enabled, Cardia will save all the data received from the heart rate monitor in a file. Format and destination of the file can be selected in the Log tab.

#### Network

![Multiplayer interface](https://raw.githubusercontent.com/uwburn/cardia/master/Readme/multiplayerred.png)

The network button will set the multiplayer options: select a nickname, a port, Server mode and hit connect to host a multiplayer section.
 
_**NOTE:** Port forwarding is required for servers. If a firewall is active, it should be configured to allow Cardia networking. Old versions of Cardia are not compatible with newest versions! Always keep your software updated._
 
If another player has already opened a server, select a nickname, the server port, the server IP address, Client mode and hit connect to join the server.
Cardia currently supports up to 4 players.

![Network window](https://raw.githubusercontent.com/uwburn/cardia/master/Readme/network.png)

- - - -

### Bluetooth Smart support
Bluetooth Smart (also Bluetooth LE and Bluetooth 4.0) has been added into version 1.1 (making the old 1.0 legacy). Bluetooth smart add support for many specific uses case devices via dedicated profiles. Instead of using the generic Serial Port Profile (as with Zephyr HxM), there's a HRM profile to be used. Cardia has been successfully tested out on Polar H7 and H10 devices.
Some users reports issues about Bluetooth Smart compatibility, please ensure the following:
* You must be runnning Windows 10 1709 or later (currently is the last one)
* Your device must be paired before running Cardia

If your device works ok, please post it out [here](https://github.com/uwburn/cardia/issues/3) , this would be helpful!

- - - -

### How to get support
Since version 1.1 Cardia write logs to help out diagnose problems. Please open an issue here (other places will be ignored, sorry!) and paste the log content. Log files are located into logs directory next to Cardia.exe.

- - - -

### About the code
The idea behind this project evolved as the work on it progressed. We started on a "i would like to see the heartbeat on overlay when i'm playing" and then added the ECG simulation. After some people showed interest for the vidos posted on youTube, i published the application and added a few things we didn't really taught about at the beginning, as the multiplayer support and attempted support for other devices.

On a programmer side, i used this project to explore a clear separation of the various components and i went after a MVVM approach built upon Windows Forms, using extensively events. Also, the network code, while simplicistic, was interesting to work on.

Now, about two years after i started working on it, looking back at the code it seems a bit messy in some parts, so if you're trying to contribute i apologize for it. Also, sorry for not using a dependency manager. I'm much more a Java guy (and i would not start a project without Maven there), so i didn't know what to use and just copied the DLLs. Should not be a great problem since there's only 3. 

- - - -

### Donations
If you would like to support me and help me buy the devices to include support for Bluetooth Smart, please consider making a donation on my PayPal account using the button below.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NYEKDVZMWSLJG)

- - - -

### Changelog
* v1.2.0.1 (2022-08-03)
  * Change BT HRP initialization sequence (thanks to [@effgenesis](https://www.github.com/effgenesis))

* v1.2 (2018-12-01)
  * Included UDP logging support
  * Partial code rewrital to support better modularity
  * Additional logging instruction to track down BT smart pairing issues

* v1.1 (2018-01-14)
  * Splitted main and legacy version
  * Ensured network compatibility between main and legacy version
  * Bluetooth Smart support on main version

* v1.0 (2015-12-20)
  * Code posted on GitHub
  * Removed expiry date

* v0.9.5412 (2014-11-29)
  * Major network bug fix

* v0.8.5398 (2014-11-16)
  * Minor bug fixes
  * Added compatibility with CMS50 Oximeter

* v0.7.5280 (2014-06-16)
  * Fixed a bug in network coding

* v0.7.5277 (2014-06-14)
  * Fixed major bug in network panel causing silent crash in network thread

* v0.7.5271 (2014-06-10)
  * Fixed a crash preventing chart initialization
  * Fixed a crash when exiting the program

* v0.7.5267 (2014-06-03)
  * Minor bug fixes

* v0.7.5266 (2014-06-02)
  * Major code rewrital of the UI, allowing for better manintainability and future expandibility
  * Fixed a bug in the netcode causing a memory leak
  * Added a timeout period for the Zephyr HxM
  * Enforced version check in the netcode
  * More explanitory error message in the network part
  * Added alarm functionality
  * Added sound functionality
  * Changed version numbering system
