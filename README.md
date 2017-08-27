# JavaScript-Debugger
A debugger for clientside javascript.


This tool allows you to keep track of your javascript variable values in realtime.

How does it work?
This program setups a local websocket-server on your computer. The javascript file that you add into your HTML-code sends the variables to the server.

Usage:

Add this to your HTMLhead.
```HTML
<head>
  <script src=""/>
</head>
```

Run the JavascripDebugger.exe

Add this to your javascript

```javascript
  d.start();
  
  var object = {};
  object.x = 42;
  //d.trk("var-name",variable,"target-property");
  d.trk("object",object,"x");
  
  var global = 42;
  //d.trk("var-name",var);
  d.trk("global",global);
  
  
  //now change the property x of variable object.
```
