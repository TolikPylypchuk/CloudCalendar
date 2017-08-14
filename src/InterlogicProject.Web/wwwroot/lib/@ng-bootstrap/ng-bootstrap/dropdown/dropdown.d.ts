import { EventEmitter, ElementRef } from '@angular/core';
import { NgbDropdownConfig } from './dropdown-config';
/**
 */
export declare class NgbDropdownMenu {
    dropdown: any;
    private _elementRef;
    isOpen: boolean;
    constructor(dropdown: any, _elementRef: ElementRef);
    isEventFrom($event: any): any;
}
/**
 * Allows the dropdown to be toggled via click. This directive is optional.
 */
export declare class NgbDropdownToggle {
    dropdown: any;
    private _elementRef;
    constructor(dropdown: any, _elementRef: ElementRef);
    toggleOpen(): void;
    isEventFrom($event: any): any;
}
/**
 * Transforms a node into a dropdown.
 */
export declare class NgbDropdown {
    private _menu;
    private _toggle;
    /**
     * Indicates that the dropdown should open upwards
     */
    up: boolean;
    /**
     * Indicates that dropdown should be closed when selecting one of dropdown items (click) or pressing ESC.
     * When it is true (default) dropdowns are automatically closed on both outside and inside (menu) clicks.
     * When it is false dropdowns are never automatically closed.
     * When it is 'outside' dropdowns are automatically closed on outside clicks but not on menu clicks.
     * When it is 'inside' dropdowns are automatically on menu clicks but not on outside clicks.
     */
    autoClose: boolean | 'outside' | 'inside';
    /**
     *  Defines whether or not the dropdown-menu is open initially.
     */
    _open: boolean;
    /**
     *  An event fired when the dropdown is opened or closed.
     *  Event's payload equals whether dropdown is open.
     */
    openChange: EventEmitter<{}>;
    constructor(config: NgbDropdownConfig);
    /**
     * Checks if the dropdown menu is open or not.
     */
    isOpen(): boolean;
    /**
     * Opens the dropdown menu of a given navbar or tabbed navigation.
     */
    open(): void;
    /**
     * Closes the dropdown menu of a given navbar or tabbed navigation.
     */
    close(): void;
    /**
     * Toggles the dropdown menu of a given navbar or tabbed navigation.
     */
    toggle(): void;
    closeFromClick($event: any): void;
    closeFromOutsideEsc(): void;
    private _isEventFromToggle($event);
    private _isEventFromMenu($event);
}
