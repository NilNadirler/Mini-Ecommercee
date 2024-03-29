import { NgxSpinnerService } from 'ngx-spinner';
import { Component} from '@angular/core';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent  {

  constructor( private spinner:NgxSpinnerService) { }

   showSpinner(spinnerNameType:SpinnerType){
     
    this.spinner.show(spinnerNameType);

    setTimeout(()=> this.hideSpinner(spinnerNameType), 1000)
   }

   hideSpinner(spinnerNameType:SpinnerType){

    this.spinner.hide(spinnerNameType);
   }

  
}
export enum SpinnerType{
     BallAtom= "s1",
     BallScaleMultiple = "s2",
     BallSpinClockwiseFadeRotating ="s3"

}
