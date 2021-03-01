# CSVDataConverter

## **TASK** 

Create a C# console application that can be given a CSV file and convert it to XML or JSON.

Your solution should be written with possible future requirements in mind, e.g. converting back to
CSV from other XML or JSON, converting between XML and JSON, or possibly taking input from a
different source, e.g. a database.
CSV file format
- The first line will contain column headings
- The headings should form keys in the XML or JSON output
- Underscores should be used to group headings, e.g.:

        name,address_line1,address_line2
        Dave,Street,Town
        
            should convert to
            {
                name: Dave,
                address: {
                    line1: Street,
                    line2: Town
                }
            }

## **IMPROVEMENTS** 

I would look to improve this solution in the following ways:
1. Allow for multiple underscores to create deeper nesting of data - the current approach only handles one underscore correctly.
2. Change the user interaction to be via CLI arguments - this will enable the app to be more flexible e.g. this will allow scripting and also will enable the app to be used in different scenarios, not only via the console.
3. Expand the unit test solution with additional tests - e.g. testing for multiple underscores, valid user input.
4. Introduce better error handling for invalid user inputs - the current approach just terminates the program.
