# MarvelsoftGUI

This is a WinForms application built with .NET Core 3.1 and can be ran on any Windows instance with a .NET Core preinstalled.

### Responsibility

Main responsibility of this Windows application is to consume a CSV file previously created with the **MarvelsoftConsole** utility, parse it and show it's records in a `ListView` component, where each record is represented by a different color depending on their filename. First record in the list will receive a `DarkGreen` color and all next records containing that file name, while others will be colored with `Blue`.

#### Additional features

Since loading 20000+ something records directly in the UI is a bad design idea, in order to reduce such overhead work, a pagination functionality is introduced at the bottom of the `ListView` component.

At first, when you load and process data, you can iterate **Next** and to the **Last** page of the record set. As soon as you move from page **1**, you will be able to go **Prev** or back to the **First** record, if need to.

Pagination, or let's say, the `ListView` updater, handles any `IndexOutOfRange` exceptions while iterating through pages.

`ListView` also has a `DoubleClick` handler which will open a new Window dialog which has no default Windows controls such as, *Minimize*, *Maximize* and Close, rather it's a borderless Window colored in background of the selected item from the list view, but it's also movable and this functionality demonstrates implementation of Native Win32 API by using `ReleaseCapture()` export from `user32.dll` inside a WinForms application.

Since the main requirement for this task was a custom User (reusable) Control, there's an additional child Window opened as a dialog window. When you run the base application, you will see a blue button labeled "Show in Child window", hit it, and a new Window will pop up, which will contain the same custom user control, reused on multiple places.

#### Third-party dependencies

There is only one third-party dependency on which this entire project revolves on: **CsvHelper**.

That dependency is also used on **MarvelsoftConsole** and in this project we use it only to process the CSV file, not to create any new ones.

---

### Preview

You can find `MarvelsoftGUI.exe` in either release of this project, and the application looks like this:

![MarvelsoftGUI](https://i.imgur.com/XRmrXI3.png)

White application title container and the dark grey area is the entire custom control, along with controls on it.

---

### Structure

As opposed to **MarvelsoftConsole** project, here we won't be documenting specific classes, but rather forms and controls, and their functionalities.

#### Forms

- `FormMain` - A fixed-size Window containing a custom control (seen in the picture: gray area along with application title), blue and red buttons, used to open a child window who also contains that same control, but shown but differently, and to exit application, respectively. Also features a `MenuStrip` component on the top of the Window which opens `FileDialog` from the custom control itself, and contains other menu options as well.
- `FormChild` - A fixed-size Window dialog containing only the custom control and a red exit button, which mimics the functionality of the main window, by reusing the same custom control.
- `FormItem` - a borderless, control-less Window, colored in different `BackColor`, depending which `ListView` item opened it, containing detailed record information from the selected row.

#### Custom (user and reusable) Controls

- **MarvelsoftCSVReaderControl** - this custom control does most of the work of this requirement, so let's start:
  - It contains a read-only text field who's value is updated after you successfully browse a file
  - *Browse...* button which opens Windows' native `FileDialog` to select a CSV file from the file system. It's file filter is set only to *.csv* files.
  - Information label or *LblProcess* which is updated (i.e. *Processed N records...*) on each 100th iteration of consumed CSV records and when the process is finished, stating *Finished processing...*.
  - `ListView` which is hidden by default and is shown as soon as we process records. This control will contain all CSV records shown in requested manner.
  - Pagination components:
    - Per-page - allows you to select how many items you want to see per page
    - First - moves you back to the first page of the record set
    - Prev - moves you one page back from the current page
    - Next - moves you one page forward from the current page
    - Last - moves you forward to the last page of the record set

#### Custom Control Workflow

Since the Custom Control does most of the work, it has most of the business logic of this project, so, let's begin:

1. After CSV file selection is successful, a new thread will be started, which will be ran in the background which will conduct the following:

   1. A `StreamReader` will be created by opening the selected file and we will use that reader with the `CsvReader`

   2. We setup settings for CSV reader, such as: delimiter (`;`), ignoring blank lines and missing fields, telling parser that there is no *header* record, and auto-mapping each result into the `CsvProduct` model-class.

   3. Then we grab all records and convert them to a `List<CsvProduct>` list

   4. After that, we iterate through all those records, we create different colors of each records, depending on their respective file name, we then update the UI thread by showing how many items have been processed so far, and finally we push new items into the `LoadedProducts` list of type `List<ListViewItem>` which we will later use to show records in the list view. In this process we also pass `ForeColor` to each list view item.

      When the iteration is done, we will update the UI by showing first set of records depending on the Per-page selection.

      Also, it will convert the `Price` value into your system's current currency and show it as-is in the list view. (by using `CultureInfo.CurrentCulture`)

   5. After all four steps above, the main responsibility of this control is finished. It has consumed, processed, delegated and then updated the UI with the set of records.

   6. **Exceptions and problems:** those may occur if your CSV file is broken, marking it not usable and informing you with an error message. The process will not continue if this occurs.

2. Showing information window for each list view item, on double click:

   1. When you click on any item in the list view, you will be presented with a child-window containing no default windows controls (such as close, maximize or minimize), but will contain information about your selected records and they can be copied from there. You can close that window by pressing close button on the bottom of the window. Also, the Window is movable, and that functionality demonstrates implementation of Native Win32 API in a .NET Windows Forms application.

3. Pagination

   1. Per-page size: this control contains multiple options, ranging from 10 to 1000. Defaults to 200, and by changing this value, the UI will be updated with new number of pages which is calculated instantly. The higher number you choose, more slower the application might load items in the list view.
   2. First, Prev, Next and Last: allows you to move the record set and show items from different pages, depending on your total record set and selected per-page size value.

4. Child Window:

   1. Child Window basically has all the same functionalities as does the main Window. Go ahead! Run the application and test it.

---

### Final developer notes

This project was initially started as a .NET Framework 4.5 Windows Forms Application. It was designed in that environment, but it worked very slow as opposed to .NET core implementation. Most of the code from .NET Framework to .NET Core does not differ much.

Since the Windows Forms Designer does not still work fully and properly under .NET Core, this way creating it show's it wasn't a bad decision after all.

Finally, there are no unit tests for this application, yet.

The code of this application might have been refactored and reorganized in a more better manner, but for the sake of deadlines, currently this is how it's implemented for the time being.

