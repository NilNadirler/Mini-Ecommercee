import { FileUploadOptions } from './../../../../services/common/file-upload/file-upload.component';

import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Create_Product } from './../../../../contracts/create_product';
import { ProductService } from './../../../../services/common/model/product.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent extends BaseComponent implements OnInit {
  constructor(
    spinner: NgxSpinnerService,
    private productService: ProductService,
    private toastr: ToastrService
  ) {
    super(spinner);
  }

  ngOnInit(): void {}

  @Output() createdProduct: EventEmitter<Create_Product> = new EventEmitter();
  @Output() fileUploadOptions: Partial<FileUploadOptions> = {
    action: 'upload',
    controller: 'products',
    explanation: 'Resimleri surukleyin veya secin',
    isAdminPage: true,
    accept: '.png, .jpg, .jpeg, .json',
  };

  create(
    name: HTMLInputElement,
    stock: HTMLInputElement,
    price: HTMLInputElement
  ) {
    this.showSpinner(SpinnerType.BallAtom);

    const create_product: Create_Product = new Create_Product();
    create_product.name = name.value;
    create_product.stock = parseInt(stock.value);
    create_product.price = parseFloat(price.value);

    this.productService.create(
      create_product,
      () => {
        this.hideSpinner(SpinnerType.BallAtom);
        this.toastr.success('Ürün başarıyla eklenmiştir.');
        this.createdProduct.emit(create_product);
      },
      (errorMessage: string | undefined) => {
        this.toastr.error(errorMessage);
      }
    );
  }
}
