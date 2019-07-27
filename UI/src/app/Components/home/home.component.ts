import { FormControl, FormGroup, Validators } from '@angular/forms';

import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/Services/api.service';
import { HttpEventType } from '@angular/common/http';
import { TestFormRequest } from 'src/app/Models/test-form-model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public progress: number;
  public message: string;
  formTest: FormGroup;

  testFormRequest : TestFormRequest = {
    Identification : undefined,
    formFile: undefined,
    
  };
  constructor(private apiService: ApiService) { 

    this.formTest = new FormGroup({
      'Identification' : new FormControl('', [Validators.required]),
      'fileData': new FormControl(null, [Validators.required])
    });
    
  }

  ngOnInit() {
  }
  onFileSelect(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.formTest.get('fileData').setValue(file);
    }
  }
  onSubmit() {
    
    this.testFormRequest.Identification= (this.formTest.get('Identification').value);
    const formData = new FormData();
    formData.append('file', this.formTest.get('fileData').value);    
    formData.append('Identification', this.formTest.get('Identification').value);    
    console.log(formData);
  this.testFormRequest.formFile= formData;
    this.apiService.UploadFile(formData).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response)
        this.message = event.body.toString();
    });
  }

}
