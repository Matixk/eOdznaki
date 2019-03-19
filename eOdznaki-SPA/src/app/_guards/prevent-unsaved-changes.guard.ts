import {Injectable} from '@angular/core';
import {CanDeactivate} from '@angular/router';
import {ProfileEditComponent} from '../components/profile-edit/profile-edit.component';

@Injectable()
export class PreventUnsavedChanged implements CanDeactivate<ProfileEditComponent> {
  canDeactivate(component: ProfileEditComponent) {
    if (component.editForm.dirty) {
      return confirm('Are you sure you want to continue?\nAny unsaved changed will be lost.');
    }
    return true;
  }
}
