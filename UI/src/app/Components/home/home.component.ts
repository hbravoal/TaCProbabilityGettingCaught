import { FormControl, FormGroup, Validators } from '@angular/forms';

import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/Services/api.service';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public progress: number;
  public message: string;
  formTest: FormGroup;
  constructor(private apiService: ApiService) { 

    this.formTest = new FormGroup({
      'Identification' : new FormControl('', [Validators.required])
    });
  }

  ngOnInit() {
  }

  upload(files) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (let file of files)
      formData.append(file.name, file);

    // const uploadReq = new HttpRequest('POST', `api/upload`, formData, {
    //   reportProgress: true,
    // });

    this.apiService.UploadFile(formData).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response)
        this.message = event.body.toString();
    });
  }

}
