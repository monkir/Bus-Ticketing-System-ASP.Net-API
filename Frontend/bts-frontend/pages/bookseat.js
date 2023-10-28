export default function BookSeat() {
    const numbers = Array.from({ length: 40 }, (value, index) => index + 1);
    const booked_seat = [1,5,9,11,25,35];
    return (
        <>
            <div className=" justify-center">
                <h3 className="mb-5 text-lg font-medium text-gray-900 dark:text-white">Choose technology:</h3>
                <ul className="grid w-3/4 gap-6 md:grid-cols-4">
                    {numbers.map((i) =>
                        <li key={i}>
                            {
                                booked_seat.includes(i)
                                ? <input type="checkbox" disabled id={"seat" + i} name="seat_no[]" value={i} className="hidden peer" required="" />
                                : <input type="checkbox"  id={"seat" + i} name="seat_no[]" value={i} className="hidden peer" required="" />
                            }
                            <label htmlFor={"seat" + i} className="inline-flex items-center justify-center w-full p-5 text-gray-500 bg-white peer-checked:bg-blue-400 peer-disabled:bg-red-300 border-2 border-gray-200 rounded-lg cursor-pointer dark:hover:text-gray-300 dark:border-gray-700 peer-checked:border-blue-600 hover:text-gray-600 dark:peer-checked:text-gray-300 peer-checked:text-gray-600 hover:bg-gray-50 dark:text-gray-400 dark:bg-gray-800 dark:hover:bg-gray-700">
                                <div className="block">
                                    <div className="w-full text-lg font-semibold">{i}</div>
                                </div>
                            </label>
                        </li>
                    )}
                </ul>
            </div>
        </>
    )
}