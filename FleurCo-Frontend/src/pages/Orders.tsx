import { createResource, createSignal, For } from "solid-js"
import Layout from "../components/Layout"
import { getOrders, Order } from "../utils/api"
import { useNavigate } from "@solidjs/router";

export const Orders = () => {
    const [orders] = createResource<Order[]>(getOrders);
    const [search, setSearch] = createSignal("");
    const navigate = useNavigate();


    function convertToEasternTime(timeString: string) {
        const isoString = timeString.replace(' ', 'T') + 'Z';
        const date = new Date(isoString);

        const options: Intl.DateTimeFormatOptions = {
            timeZone: 'America/New_York',
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit',
            hour12: false,
        };

        const formatter = new Intl.DateTimeFormat('en-US', options!);

        const parts = formatter.formatToParts(date);
        const dateParts: { [key: string]: string } = {};

        parts.forEach(({ type, value }) => {
            dateParts[type] = value;
        });

        const easternTime = `${dateParts.year}-${dateParts.month}-${dateParts.day} ${dateParts.hour}:${dateParts.minute}:${dateParts.second}`;

        return easternTime;
    }
    const fileredorders = () => {
        if (search() === "") {
            return orders();
        }
        return orders()?.filter((order) =>
            order.orderTotal.toString().includes(search().toLowerCase())
            || order.orderDate.toString().includes(search().toLowerCase())
            || order.orderType.toLowerCase().includes(search().toLowerCase())
            || order.orderStatus.toLowerCase().includes(search().toLowerCase())
            || order.orderId.toLowerCase().includes(search().toLowerCase())
        );
    }
    const handleKeyDown = (e: KeyboardEvent, orderId: string) => {
        if (e.key === "Enter" || e.key === " ") {
            e.preventDefault();
            navigate(`/inventory/${orderId}/products`);
        }
    };
    return (
        <Layout>
            <div class="flex flex-col h-full w-full gap-4">
                <div class="px-2 flex flex-col gap-4">
                    <div class="flex flex-row justify-between items-center">
                        <h1 class="text-3xl font-bold"> Orders </h1>
                        <a href="/backorders/new" class="btn btn-primary">Create Backorder</a>
                    </div>
                    <label class="input input-bordered flex items-center gap-2">
                        <input type="text" class="grow" placeholder="Search by ID, Date, Type, Status, Order Total..." value={search()} onInput={(e) => setSearch(e.currentTarget.value)} />
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 16 16"
                            fill="currentColor"
                            class="h-4 w-4 opacity-70">
                            <path
                                fill-rule="evenodd"
                                d="M9.965 11.026a5 5 0 1 1 1.06-1.06l2.755 2.754a.75.75 0 1 1-1.06 1.06l-2.755-2.754ZM10.5 7a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0Z"
                                clip-rule="evenodd" />
                        </svg>
                    </label>
                </div>

                <div class="overflow-x-auto p-2">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Type</th>
                                <th>Status</th>
                                <th>Order Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <For each={fileredorders()}>
                                {(order) => (
                                    <tr class="hover rounded-md focus:outline-none focus:ring focus:ring-base-content/20 focus:ring-offset-base-200 focus:ring-offset-2"
                                        role="button"
                                        tabIndex="0"
                                        onKeyDown={(e) => handleKeyDown(e, order.orderId)}
                                        onClick={() => navigate(`/orders/${order.orderId}`)}
                                    >
                                        <td>{convertToEasternTime(order.orderDate)}</td>
                                        <td>{order.orderType}</td>
                                        <td>{order.orderStatus}</td>
                                        <td>{order.orderTotal}</td>
                                    </tr>
                                )}
                            </For>
                        </tbody>
                    </table>
                </div>
            </div>
        </Layout>
    )
}
export default Orders
