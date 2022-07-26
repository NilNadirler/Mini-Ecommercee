import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Component, Input } from '@angular/core';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import {
  FileUploadDialogComponent,
  FileUploadDialogState,
} from 'src/app/dialogs/file-upload-dialog/file-upload-dialog.component';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss'],
})
export class FileUploadComponent {
  constructor(
    private httpClientService: HttpClientService,
    private toastr: ToastrService,
    private dialog: MatDialog,
    private dialogService: MatDialog
  ) {}

  public files: NgxFileDropEntry[];

  @Input() options: Partial<FileUploadOptions>;

  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();
    for (const file of files) {
      (file.fileEntry as FileSystemFileEntry).file((_file: File) => {
        fileData.append(_file.name, _file, file.relativePath);
      });
    }
    this.dialogService.openDialogs(() => {
      this.httpClientService
        .post(
          {
            controller: this.options.controller,
            action: this.options.action,
            queryString: this.options.queryString,
            headers: new HttpHeaders({ responseType: 'blob' }),
          },
          fileData
        )
        .subscribe(
          (data) => {},
          (errorResponse: HttpErrorResponse) => {
            if (this.options.isAdminPage) {
              this.toastr.success('Success');
            } else {
              this.toastr.success(' Success For Admin');
            }
          }
        );
    });
  }
}

export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
}
