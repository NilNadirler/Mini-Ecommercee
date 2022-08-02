import { DialogService } from './../../services/common/dialog.service';
import { ToastrService } from 'ngx-toastr';

import {
  DeleteDialogComponent,
  DeleteState,
} from './../../dialogs/delete-dialog/delete-dialog.component';

import { SpinnerType } from 'src/app/base/base.component';
import { ProductService } from './../../services/common/model/product.service';
import {
  Directive,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
  Output,
  Renderer2,
} from '@angular/core';
import {
  async,
  __core_private_testing_placeholder__,
} from '@angular/core/testing';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatDialog } from '@angular/material/dialog';
import { HttpClientService } from 'src/app/services/common/http-client.service';

declare var $: any;

@Directive({
  selector: '[appDelete]',
})
export class DeleteDirective {
  constructor(
    private element: ElementRef,
    private _render: Renderer2,
    private httpClientService: HttpClientService,
    private spinner: NgxSpinnerService,
    public dialog: MatDialog,
    private toastr: ToastrService,
    private dialogService: DialogService
  ) {
    const img = _render.createElement('img');
    img.setAttribute('src', '../../../../../assets/delete.png');
    img.setAttribute('style', 'cursor:pointer;');
    img.width = 25;
    img.height = 25;
    _render.appendChild(element.nativeElement, img);
  }

  @Input() id: string;
  @Input() controller: string;

  @Output() callback: EventEmitter<any> = new EventEmitter();

  @HostListener('click')
  async onClick() {
    this.dialogService.openDialog({
      componentType: DeleteDialogComponent,
      data: DeleteState.Yes,
      afterClosed: async () => {
        this.spinner.show(SpinnerType.BallAtom);
        const td: HTMLTableCellElement = this.element.nativeElement;
        await this.httpClientService
          .delete(
            {
              controller: this.controller,
            },
            this.id
          )
          .subscribe(
            () => {
              $(td.parentElement).animate(
                {
                  opacity: 0,
                  left: '+=50',
                  height: 'toogle',
                },
                700,
                () => {
                  this.callback.emit();
                  this.toastr.success('Basariyla silindi');
                }
              );
            },
            (errorMessage: string | undefined) => {
              this.spinner.hide(SpinnerType.BallAtom);
              this.toastr.error('Hata');
            }
          );
      },
    });
  }
}
