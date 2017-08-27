# JavaScript-Debugger
A debugger for clientside javascript.


This tool allows you to keep track of your javascript variable values in realtime.

How does it work?
This program setups a local websocket-server on your computer. The javascript file that you add into your HTML-code sends the variables to the server.

Usage:

Add this to your HTMLhead.
```HTML
<head>
  <script src="https://cdn.rawgit.com/Bambi-pa-hal-is/Javascript-Debugger/62ccf294/Javascript/Debug.js"/>
</head>
```

Run the JavascripDebugger.exe

Code example:

```javascript
  d.start(); //Start the debugger.
  //d.setProduction(); //Run this function if the environment is for production. This allows you to keep your debug code while in production. It replaces all functions with empty functions.
  
  var object = {};
  object.x = 42;
  //d.trk("var-name",variable,"target-property");
  d.trk("object",object,"x");
  
  var global = 42;
  //d.trk("var-name",var);
  d.trk("global",global);
  
  
  //now change the property x of variable object.
```
