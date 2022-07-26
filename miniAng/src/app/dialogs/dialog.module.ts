import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatDialogModule } from '@angular/material/dialog';

import { FileUploadDialogComponent } from './file-upload-dialog/file-upload-dialog.component';

@NgModule({
  declarations: [FileUploadDialogComponent, DeleteDialogComponent],
  imports: [CommonModule, MatDialogModule, MatButtonModule],
})
export class DialogModule {}
