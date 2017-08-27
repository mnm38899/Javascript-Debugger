console.log("loaded");
var ready = false;

function Debugger()
{
    var isRunning = false;
    this.intervalId==null;
    this.trackList = [];
    this.ip = "127.0.0.1";
    this.port = "30001";
    //var socket = new WebSocket
    this.socket;
    var self = this;
    
    
    this.setProduction = function()
    {
        this.start = empty;
        update = empty;
    };
    
    this.start = function()
    {
        if(isRunning==false)
        {
            this.socket = new WebSocket('ws://'+this.ip+':'+this.port);
            this.socket.onopen = connected;
            this.socket.onclose = close;
            this.socket.onerror = onerror;
            this.socket.onmessage = recieveMessage;
        }
        else
        {
            throw "Error. Debugger is already running."
        }
        
    };
    
    var connected = function(event) //when connection is ready.
    {
        console.log("Its fuking wårking");
        self.intervalId = setInterval(update,1); 
        
    };
    
    var close = function(event) //If the connection dies
    {
        //isConnectionReady=false;
        clearInterval(self.intervalId);
        console.log("Connection lost.");
    }
    
    var onerror = function (error) {
        console.log('WebSocket Error ' + error);
    };
    
    var recieveMessage = function(event)
    {
        console.log(event);
    }
    
    
    this.track = function(name, value)
    {
        let _debugObj = {};
        _debugObj.name=name;
        _debugObj.value=value;
        _debugObj.isGlobal = window[name] == undefined ? false : true;
        this.trackList.push(_debugObj);
    };
    
    this.stop = function()
    {
        this.socket.close();
    };
    

    var update = function()
    {
        //console.log("send");
        updateGlobals();
        
        let br = new basicRequest(0,JSON.stringify(self.trackList));
        
        self.socket.send(JSON.stringify(br));
        //self.socket.send("Hemligt meddelande!" + ~~(Math.random()*10));
    };
    
    var updateGlobals = function()
    {
        for(var i=0;i!=self.trackList.length;i++)
        {
            var obj = self.trackList[i];
            if(obj.isGlobal==true)
            {
                 obj.value = window[obj.name];
            }
        }
    };
    
    var empty = function()
    {
        
    };
    
    var basicRequest = function(id,data)
    {
        this.id=id;
        this.time=new Date().getTime().toString();
        this.data=data;
    }
}

var d = new Debugger();


