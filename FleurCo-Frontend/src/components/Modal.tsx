import { Component } from "solid-js";
import Toast from "./Toast";
import { MountableElement, Portal, render } from "solid-js/web";

interface ModelProps {
    btnText: string;
    btnClass: string;
    confirmText: string;
    successText?: string;
    onSuccess?: () => Promise<void>;
}
export const Modal: Component<ModelProps> = (props) => {
    var id: HTMLDialogElement | null = null;

    const onNo = () => {
        id?.close();
    }
    const onYes = async () => {
        await props.onSuccess?.();
        id?.close();
        if (props.successText) {
            const portalId = document.getElementById("portal");
            render(() => (
                <Portal>
                    <Toast text={props.successText!} duration={2500} />
                </Portal>
            ), portalId as MountableElement);
        }
    }
    return (
        <>
            <button class={props.btnClass} onClick={() => id?.showModal()}>{props.btnText}</button>
            <dialog ref={id!} class="modal">
                <div class="modal-box">
                    <h3 class="text-lg font-bold">Confirm!</h3>
                    <p class="py-4">{props.confirmText}</p>
                    <div class="flex gap-4 w-full justify-end">
                        <button class="btn btn-neutral" onClick={onYes}>Yes</button>
                        <button class="btn btn-ghost" onClick={onNo}>No</button>
                    </div>
                </div>
                <form method="dialog" class="modal-backdrop">
                    <button>close</button>
                </form>
            </dialog>
        </>
    )
}

export default Modal;