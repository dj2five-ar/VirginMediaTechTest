# VirginMediaTechTest
## Design
The project is designed to summarise data from a specific CSV format. After careful consideration I have decided to display the filtered rows of data with summary alongside it. The reason for this was based on the fact that a summary of data can mean different things to different people. A traditional pie or bar chart only display a certain aspect of data which may not be what the customer wanted. By highlighting a summary and then display rows the user will be able to access more data.
Below is a list of points
- By default the rows are toggled off to make the app work with larger data files.
- Using floats instead of doubles in model data to keep memory usage to a minimum.
- Kept error view handling to a minimum.
- The input data provided had certain inconsistencies (e.g. whitespace, empty fields) which have been handled. A missing number field will by default result to 0. An incorrect number field e.g. 3.0.3 will display an error as even one field can influence the averages or profits or revenues. It will be up to the operator to rectify the fields before inputting.
- I have assumed the date is in British format of DD/MM/YY. 
## Tests 
Unit tests have been created to test certain aspects of the service and controller.