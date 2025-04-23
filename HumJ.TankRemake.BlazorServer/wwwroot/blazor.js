{
    class MyWebSocket extends WebSocket {
        constructor(url, protocols) {
            super(url, protocols);

            if (url.indexOf('_blazor') > 0) {
                this.isBlazor = true;

                this.addEventListener('message', ev => {
                    //console.log('receive', ev)
                    this.byteRx = ~~this.byteRx + ev.data.byteLength;
                });

                var _this = this;
                window.getBlazorTraffic = function () {
                    var result = [~~_this.byteTx, ~~_this.byteRx];
                    _this.byteTx = _this.byteRx = 0;
                    return result;
                }
            }
        }

        send(data) {
            super.send(data)

            if (this.isBlazor) {
                //console.log('send', data);
                this.byteTx = ~~this.byteTx + data.byteLength;
            }
        }
    }

    window.WebSocket = MyWebSocket;
}