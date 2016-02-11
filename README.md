echo
====

A light-weight, in-process HTTP simulator for easy functional testing.

Usage
-----
```c#
//  Start the simulator on http://localhost:3000
using (var simulator = Simulator.Start(3000))
{
    //  Configure a response for "/greeting"
    simulator.Responses.Add(
        new Response(
            rule: r => r.RequestUri.PathAndQuery == "/greeting",
            message: new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Hello World"),
            }
        )
    );

    //  Go ahead and visit http://localhost:3000/greeting in your browser...
    WebRequest
        .Create("http://localhost:3000/greeting")
        .GetResponse().Dispose();

    //  The simulator collects any requests for verification later
    var request = simulator.Requests.Single();

    Console.WriteLine(request.Method);
    Console.WriteLine(request.RequestUri);
    Console.WriteLine(request.Content);        
}
```

License
-------

Copyright � Colin Orr 2013

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see http://www.gnu.org/licenses/.
