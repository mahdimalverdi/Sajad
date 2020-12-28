import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContentService {

  constructor(private readonly client: HttpClient) { }

  public async uploadFile(fileToUpload: File): Promise<any> {
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    return await this.client.post('/api/Contents/UploadFile', formData).toPromise();
  }
}
