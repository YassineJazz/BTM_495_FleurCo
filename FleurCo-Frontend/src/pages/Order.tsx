import { useNavigate, useParams } from "@solidjs/router";
import Layout from "../components/Layout";
import { createResource, For } from "solid-js";
import { Order, OrderProduct, getOrder, getOrderProducts } from "../utils/api";
import { convertToEasternTime } from "../utils/date";

const OrderPage = () => {
    const params = useParams();
    const navigate = useNavigate();

    const [order] = createResource<Order>(() => getOrder(params.orderId));
    const [products] = createResource<OrderProduct[]>(() => getOrderProducts(params.orderId));

    const goBack = () => {
        navigate("/orders");
    }

    return (
        <Layout>
            <div class="flex flex-col w-full overflow-auto gap-4 p-4">
                <div class="flex flex-col w-full gap-4">
                    <div class="flex flex-col gap-2">
                        <h1 class="text-3xl font-bold"> FleurCo </h1>
                        <div class="flex flex-row justify-between items-center">
                            <h2 class="text-3xl font-semi-bold"> Orders </h2>
                            <div class="flex flex-row justify-center items-center">
                                <svg xmlns="http://www.w3.org/2000/svg" width="36" height="36" viewBox="0 0 24 24"><path d="M19 7.001c0 3.865-3.134 7-7 7s-7-3.135-7-7c0-3.867 3.134-7.001 7-7.001s7 3.134 7 7.001zm-1.598 7.18c-1.506 1.137-3.374 1.82-5.402 1.82-2.03 0-3.899-.685-5.407-1.822-4.072 1.793-6.593 7.376-6.593 9.821h24c0-2.423-2.6-8.006-6.598-9.819z" /></svg>                            <div class="flex flex-col justify-start items-start">
                                    <label class="text-3x1 font-bold text-base"> Bryan Doe</label>
                                    <label class="text-3x1 font-semi-bold text-sm">Warehouse Manager</label>
                                </div>
                            </div>

                        </div>
                        <div class="flex flex-col items-start gap-4">
                            <button class="btn btn-sm btn-neutral" onClick={goBack}>Back</button>
                            <p class="text-2xl font-semibold">Order ID: {order()?.orderId}</p>
                        </div>
                    </div>
                </div>
                <div class="flex flex-col gap w-full h-full gap-2">
                    <div class="flex flex-col items-start">
                        <label for="date" class="label gap-2"> <span class="font-bold"> Date: </span>{order()?.orderDate ? convertToEasternTime(order()?.orderDate!) : ""}</label>
                    </div>
                    <div class="flex flex-col items-start">
                        <label for="date" class="label gap-2"> <span class="font-bold"> Type: </span>{order()?.orderType}</label>
                    </div><div class="flex flex-col items-start">
                        <label for="date" class="label gap-2"> <span class="font-bold"> Status: </span>{order()?.orderStatus}</label>
                    </div><div class="flex flex-col items-start">
                        <label for="date" class="label gap-2"> <span class="font-bold"> Order Total ($): </span>{order()?.orderTotal.toFixed(2)}</label>
                    </div>

                    <h1 class="text-lg font-semibold"> Products in this order: </h1>

                    <div class="flex overflow-x-auto">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Quantity</th>
                                    <th>Price</th>
                                    <th>Cost</th>
                                    <th>Category</th>

                                </tr>
                            </thead>
                            <tbody >
                                <For each={products()}>
                                    {(product) => (
                                        <tr
                                        >
                                            <td>{product.productName}</td>
                                            <td>{product.productQty}</td>
                                            <td>{product.productPrice.toFixed(2)}</td>
                                            <td>{product.productCost.toFixed(2)}</td>
                                            <td>{product.productCategory}</td>

                                        </tr>
                                    )}
                                </For>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
export default OrderPage;

