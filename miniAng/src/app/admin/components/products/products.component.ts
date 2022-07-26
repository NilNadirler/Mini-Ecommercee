import { ListComponent } from './list/list.component';

import { BaseComponent} from './../../../base/base.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import {NgxSpinnerService} from 'ngx-spinner'
import { Create_Product } from 'src/app/contracts/create_product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService) {
    super(spinner);
  }

  ngOnInit(): void {

    
  }

  @ViewChild(ListComponent) listComponents: ListComponent;

  createdProduct(createdProduct :Create_Product){

    this.listComponents.getProducts();
  }

}
