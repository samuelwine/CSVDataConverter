# CSVDataConverter

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
