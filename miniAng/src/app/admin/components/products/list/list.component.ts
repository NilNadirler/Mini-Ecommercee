import { MatPaginator } from '@angular/material/paginator';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from './../../../../services/common/model/product.service';

import {
  MatTableDataSource,
  _MatTableDataSource,
} from '@angular/material/table';
import { Component, OnInit, ViewChild } from '@angular/core';
import { List_Product } from 'src/app/contracts/list-product';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})



export class ListComponent extends BaseComponent implements OnInit {
  constructor(
    private productService: ProductService,
    spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) {
    super(spinner);
  }

  displayedColumns: string[] = [
    'name',
    'stock',
    'price',
    'createdDate',
    'updatedDate',
    'edit',
    'delete'
  ];

 
  async ngOnInit() {
    this.getProducts();
  }
  
  dataSource: MatTableDataSource<List_Product>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
 
  async getProducts() {
    this.showSpinner(SpinnerType.BallAtom);
    const allProducts: { totalCount: number; products: List_Product[] } |undefined = await this.productService.read(this.paginator ? 
      this.paginator.pageIndex : 0, this.paginator ? this.paginator.pageSize : 5, () => this.hideSpinner(SpinnerType.BallAtom),
       errorMessage => this.toastr.error(errorMessage))
    this.dataSource = new MatTableDataSource<List_Product>(allProducts?.products);
    this.paginator.length = allProducts?.totalCount;
  }


  async pageChanged() {
    await this.getProducts();
  }


}
