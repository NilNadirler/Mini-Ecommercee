import { FileUploadModule } from './../services/common/file-upload/file-upload.module';

import { FileUploadComponent } from './../services/common/file-upload/file-upload.component';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';

import { SelectProductImageDialogComponent } from './select-product-image-dialog/select-product-image-dialog.component';

@NgModule({
  declarations: [DeleteDialogComponent, SelectProductImageDialogComponent],
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    FileUploadModule,
    MatCardModule,
  ],
})
export class DialogModule {}
