import { DatePicker } from "antd";
import { RangePickerProps } from "antd/es/date-picker";
import dayjs, { Dayjs } from "dayjs";
import customParseFormat from "dayjs/plugin/customParseFormat";
import { useWizard } from "react-use-wizard";

dayjs.extend(customParseFormat);

interface Props {
  stepNumber: number;
  setStepNumber: React.Dispatch<React.SetStateAction<number>>;

  date: Dayjs | undefined | null;
  setDate: React.Dispatch<React.SetStateAction<Dayjs | undefined | null>>;

  finishMethod: () => void;
}

function InviteStep4({
  stepNumber,
  setStepNumber,
  date,
  setDate,
  finishMethod,
}: Props) {
  const { previousStep } = useWizard();

  const inputHandlerNext = () => {
    finishMethod();
  };

  const disabledDate: RangePickerProps["disabledDate"] = (current) => {
    // Can not select days before today and today
    return current && current < dayjs().endOf("day");
  };

  const inputHandlerPrevious = () => {
    setStepNumber(stepNumber - 1);
    previousStep();
  };

  return (
    <div className="flex items-center justify-center min-h-[450px] flex-wrap">
      <div>
        <h1 className="text-center uppercase font-bold text-[20px]">
          Finish your invitation
        </h1>
        <p className="text-center p-2">
          To join the fun and excitement of playing table board games with other
          enthusiasts, please select a date from the calendar below and confirm
          your attendance. We look forward to seeing you there!
        </p>
        <div>
          <div className=" font-bold flex justify-center">
            <p>Meet date</p>
          </div>
          <div className="p-2 flex justify-center ">
            <DatePicker
              className="bg-[#ffffff] hover:border-green-500 min-h-[50px] min-w-[200px]"
              style={{
                color: "#FFFFFF",
                fontWeight: "bold",
                colorAdjust: "revert",
              }}
              format="YYYY-MM-DD HH:mm"
              disabledDate={disabledDate}
              showTime={{ defaultValue: dayjs("00:00:00", "HH:mm") }}
              value={date}
              onChange={(e) => setDate(e)}
              dateRender={(current) => {
                return (
                  <div className="ant-picker-cell-inner">{current.date()}</div>
                );
              }}
            />
          </div>
        </div>
      </div>
      <div className="flex justify-center p-2 m-1">
        <button
          className="btn m-2 min-w-[10%]"
          onClick={() => inputHandlerPrevious()}
        >
          Previous
        </button>
        <button
          className="btn m-2 min-w-[100px]"
          onClick={() => inputHandlerNext()}
        >
          Finish
        </button>
      </div>
    </div>
  );
}

export default InviteStep4;
