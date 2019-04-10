import {Validators} from '@angular/forms';

export class FormValidatorOptions {

  static setStringOptions(required: boolean, minLength: number, maxLength: number) {
    const validators = [null, Validators.minLength(minLength), Validators.maxLength(maxLength)];
    if (required) {
      validators[0] = Validators.required;
    }

    return validators;
  }

}
