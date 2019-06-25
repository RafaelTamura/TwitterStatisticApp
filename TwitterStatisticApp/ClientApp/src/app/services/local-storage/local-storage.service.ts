import { Inject, Injectable } from '@angular/core';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
    key = CryptoJS.enc.Hex.parse('543f2cd380028b5e60e1ca4aa6f83b60');
    iv = CryptoJS.enc.Hex.parse('543f2cd380028b5e60e1ca4aa6f83b60');

    constructor(@Inject(SESSION_STORAGE) private storage: StorageService) { }

    public setOnLocalStorage(storageKey: string, obj: any): void {
        const encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(obj), this.key,
            {
                keySize: 128 / 8,
                iv: this.iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            });

        this.storage.set(storageKey, encrypted.toString());
    }

    public getOnLocalStorage(storageKey: string): any {
        let get = this.storage.get(storageKey);

        if (get !== null) {
            get = CryptoJS.AES.decrypt(get, this.key, {
                keySize: 128 / 8,
                iv: this.iv,
                mode: CryptoJS.mode.CBC,
                padding: CryptoJS.pad.Pkcs7
            }).toString(CryptoJS.enc.Utf8);
        }

        return get;
    }

    public cleanLocalStorage(storageKey: string) {
        this.storage.remove(storageKey);
    }
}
