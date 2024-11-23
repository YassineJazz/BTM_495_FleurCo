import { Component, createEffect, createSignal, Show } from "solid-js";

interface ToastProps {
    text: string;
    duration: number;
}


const Toast: Component<ToastProps> = (props) => {
    const [show, setShow] = createSignal(true);

    createEffect(() => {
        if (props.duration === 0) return;
        setTimeout(() => {
            setShow(false);
        }, props.duration);
    });

    return (
        <Show when={show()}>
            <div class="toast toast-end">
                <div class="alert bg-transparent border-0">
                    <div role="alert" class="alert alert-success">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            class="h-6 w-6 shrink-0 stroke-current"
                            fill="none"
                            viewBox="0 0 24 24">
                            <path
                                stroke-linecap="round"
                                stroke-linejoin="round"
                                stroke-width="2"
                                d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
                        </svg>
                        <span>{props.text}</span>
                    </div>
                </div>
            </div>
        </Show>

    )
}

export default Toast;
