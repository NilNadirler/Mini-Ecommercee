import { Injectable } from '@angular/core';
declare var alertify:any

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }


  message(message:string,messageType:MessageType, position:Position){
    alertify.set('notifier','position',position)
    alertify[messageType](message)
  }
}

export enum MessageType{
  Error ="Enum",
  Message ="message",
  Success ="Success",
  Notify ="notify",
  Warning ="warning"
}

export enum Position{
  TopCenter ="top-center",
  TopRight ="top-right",
  TopLeft ="bottom-right",
  BottomCenter="bottom-center",
  BottomLeft= "bottom-left"

}
